/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
enum
{
	P_DUMMY,
	P_WHITEBOX,
	P_WHITECIRCLE,

	// app > @ P_

	ENUM_RANGE(P_YOUMU_STAND_00, 8)
	ENUM_RANGE(P_YOUMU_DASH_00, 8)
	ENUM_RANGE(P_YOUMU_JUMP_00, 10)
	ENUM_RANGE(P_YOUMU_SIT_00, 4)
	ENUM_RANGE(P_YOUMU_HIT_00, 4) // _00 ‚Í STAND_00
	ENUM_RANGE(P_YOUMU_ATTACK_00, 11) // _00 ‚Í STAND_00
	ENUM_RANGE(P_YOUMU_SITATTACK_00, 11) // _00 ‚Í SIT_00_LAST
	ENUM_RANGE(P_YOUMU_JUMPATTACK_00, 10) // _00 ‚Í JUMP_00_LAST
	ENUM_RANGE(P_YOUMU_JUMPFRONT_00, 13) // ... 09, 11, 10, JUMP_00_LAST

	// < app

	P_MAX, // num of member
};

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct PicInfo_st
{
	int Handle;
	int W;
	int H;
}
PicInfo_t;

// Pic_ >

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int Pic_FileData2SoftImage(autoList<uchar> *fileData);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int Pic_SoftImage2GraphicHandle(int si_h);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
PicInfo_t *Pic_GraphicHandle2PicInfo(int handle);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Pic_ReleasePicInfo(PicInfo_t *i);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Pic_GetSoftImageSize(int si_h, int &w, int &h);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Pic_GetGraphicHandleSize(int handle, int &w, int &h);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
extern int SI_R;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
extern int SI_G;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
extern int SI_B;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
extern int SI_A;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Pic_GetSIPixel(int si_h, int x, int y);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Pic_SetSIPixel(int si_h, int x, int y);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int Pic_CreateSoftImage(int w, int h);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Pic_ReleaseSoftImage(int si_h);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Pic_ReleaseGraphicHandle(int handle);

// < Pic_

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
resCluster<PicInfo_t *> *CreatePicRes(PicInfo_t *(*picLoader)(autoList<uchar> *), void (*picUnloader)(PicInfo_t *));
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void UnloadAllPicResHandle(void);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SetPicRes(resCluster<PicInfo_t *> *resclu);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
resCluster<PicInfo_t *> *GetPicRes(void);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void ResetPicRes(void);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define DTP 0x40000000

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int Pic(int picId);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int Pic_W(int picId);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int Pic_H(int picId);
