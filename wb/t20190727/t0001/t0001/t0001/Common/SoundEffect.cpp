/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static oneObject(autoQueue<SEInfo_t *>, new autoQueue<SEInfo_t *>(), GetPlayList);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void UpdateSEVolumeFunc(SEInfo_t *i)
{
	for(int index = 0; index < SE_HANDLE_MAX; index++)
	{
		SetVolume(i->HandleList[index], MixVolume(Gnd.SEVolume, i->Volume));
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static SEInfo_t *LoadSE(autoList<uchar> *fileData)
{
	SEInfo_t *i = nb_(SEInfo_t);

	i->HandleList[0] = LoadSound(fileData);

	for(int index = 1; index < SE_HANDLE_MAX; index++)
	{
		i->HandleList[index] = DuplSound(i->HandleList[0]);
	}
	i->Volume = 0.5; // 個別の音量のデフォルト 0.0 - 1.0

	switch(RC_ResId) // seId
	{
	// app > @ post LoadSound

	/*
	case SE_xxx:
		break;
	*/

	// < app

	case -1: // dummy
	default:
		break;
	}

	UpdateSEVolumeFunc(i);
	return i;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void UnloadSE(SEInfo_t *i)
{
	GetPlayList()->Clear();

	for(int index = 0; index < SE_HANDLE_MAX; index++)
	{
		UnloadSound(i->HandleList[index]);
	}
	memFree(i);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static oneObject(
	resCluster<SEInfo_t *>,
	new resCluster<SEInfo_t *>("SoundEffect.dat", "..\\..\\SoundEffect.txt", SE_MAX, 130000000, LoadSE, UnloadSE),
	GetSERes
	);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int SEEachFrame(void) // ret: 効果音を処理した。
{
	SEInfo_t *i = GetPlayList()->Dequeue(NULL);

	if(i)
	{
		switch(i->AlterCommand)
		{
		case 0:
			i->HandleIndex %= SE_HANDLE_MAX;
			SoundPlay(i->HandleList[i->HandleIndex++]);
			break;

		case 'S':
			for(int index = 0; index < SE_HANDLE_MAX; index++)
			{
				SoundStop(i->HandleList[index]);
			}
			break;

		case 'L':
			SoundPlay(i->HandleList[0], 0);
			break;

		default:
			error();
		}
		return 1;
	}
	return 0;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SEPlay(int seId)
{
	errorCase(seId < 0 || SE_MAX <= seId);

	SEInfo_t *i = GetSERes()->GetHandle(seId);
	int count = 0;

	for(int index = GetPlayList()->GetTopIndex_DIRECT(); index < GetPlayList()->GetList_DIRECT()->GetCount(); index++)
		if(GetPlayList()->GetList_DIRECT()->GetElement(index) == i && 2 <= ++count)
			return;

	GetPlayList()->Enqueue(i);
	GetPlayList()->Enqueue(NULL);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void DoAlterCommand(int seId, int alterCommand)
{
	errorCase(seId < 0 || SE_MAX <= seId);

	static SEInfo_t sis[16]; // 長さ == 同時に処理できる個数
	static int sisi = 0;

	SEInfo_t *i = sis + sisi;
	sisi = (sisi + 1) % lengthof(sis);
	*i = *GetSERes()->GetHandle(seId); // todo: SEInfo_t の複製 <-- 問題無いか？
	i->AlterCommand = alterCommand;

	GetPlayList()->Enqueue(i);
	GetPlayList()->Enqueue(NULL);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SEStop(int seId)
{
	DoAlterCommand(seId, 'S');
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SEPlayLoop(int seId)
{
	DoAlterCommand(seId, 'L');
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void UpdateSEVolume(void)
{
	GetSERes()->CallAllLoadedHandle(UpdateSEVolumeFunc);
}
