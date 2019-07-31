/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
enum // as ResMode
{
	RC_RESMD_NONE, // 未定義 as 0
	RC_RESMD_CLUSTER,
	RC_RESMD_FILELIST,
};

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
extern int RC_ResId;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<uchar> *LoadFileDataFromCluster(char *clusterFile, int resId, int tweek);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
template <class Handle_t>
class resCluster
{
private:
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	char *ClusterFile;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	char *FileListFile;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int ResCount;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int FirstTweek;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	Handle_t (*ResLoader)(autoList<uchar> *);
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void (*ResUnloader)(Handle_t);
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	Handle_t *HandleList;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	bitList *LoadedList;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int ResMode;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	autoList<char *> *FileList;

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	autoList<uchar> *LoadFileData(int resId)
	{
		if(!this->ResMode)
			this->ResMode = accessible(this->ClusterFile) ? RC_RESMD_CLUSTER : RC_RESMD_FILELIST;

		autoList<uchar> *fileData;

		switch(this->ResMode)
		{
		case RC_RESMD_CLUSTER:
			fileData = LoadFileDataFromCluster(this->ClusterFile, resId, this->FirstTweek + resId);
			break;

		case RC_RESMD_FILELIST:
			if(!this->FileList)
			{
				this->FileList = readLines_x(readAllBytes(this->FileListFile));
				errorCase(this->FileList->GetCount() != this->ResCount);

				for(int index = 0; index < this->FileList->GetCount(); index++)
				{
					char *file = this->FileList->GetElement(index);
					errorCase(m_isEmpty(file));

					if(file[1] != ':') // ? ドライブ文字なし -> ファイルリストからの相対パスと見なす。
					{
						this->FileList->SetElement(index, xcout("..\\..\\%s", file));
						memFree(file);
					}
				}
			}
			fileData = readAllBytes(this->FileList->GetElement(resId));
			break;

		default:
			error();
		}
		return fileData;
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	autoList<int> *DerHandleList; // for Derivation.cpp

public:

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	resCluster(char *clusterFile, char *fileListFile, int resCount, int firstTweek, Handle_t (*resLoader)(autoList<uchar> *), void (*resUnloader)(Handle_t))
	{
		errorCase(m_isEmpty(clusterFile));
		errorCase(m_isEmpty(fileListFile));
		errorCase(resCount < 0 || IMAX < resCount);
		errorCase(firstTweek < 0 || IMAX < firstTweek);
		errorCase(!resLoader);
		errorCase(!resUnloader);

		this->ClusterFile = clusterFile;
		this->FileListFile = fileListFile;
		this->ResCount = resCount;
		this->FirstTweek = firstTweek;
		this->ResLoader = resLoader;
		this->ResUnloader = resUnloader;
		this->HandleList = na(Handle_t, resCount);
		this->LoadedList = new bitList();
		this->ResMode = 0;
		this->FileList = NULL;
		this->DerHandleList = NULL;
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	resCluster(const resCluster &source)
	{
		error();
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	~resCluster()
	{
		1; // このクラスはプロセス寿命、プロセス終了時以外の破棄を想定しない。
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	Handle_t GetHandle(int resId)
	{
		errorCase(resId < 0 || this->ResCount <= resId);

		if(!this->LoadedList->RefBit(resId))
		{
			autoList<uchar> *fileData = this->LoadFileData(resId);

			RC_ResId = resId;
			this->HandleList[resId] = this->ResLoader(fileData);
			this->LoadedList->PutBit(resId, 1);

			delete fileData;
		}
		return this->HandleList[resId];
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void UnloadAllHandle()
	{
		UnloadAllDer(this->GetDerHandleList()); // for Derivation.cpp

		for(int resId = 0; resId < this->ResCount; resId++)
		{
			if(this->LoadedList->RefBit(resId))
			{
				RC_ResId = resId;
				this->ResUnloader(this->HandleList[resId]);
			}
		}
		this->LoadedList->Clear();
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void CallAllLoadedHandle(void (*func)(Handle_t))
	{
		for(int resId = 0; resId < this->ResCount; resId++)
		{
			if(this->LoadedList->RefBit(resId))
			{
				RC_ResId = resId;
				func(this->HandleList[resId]);
			}
		}
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	autoList<int> *GetDerHandleList() // for Derivation.cpp
	{
		if(!this->DerHandleList)
			this->DerHandleList = new autoList<int>();

		return this->DerHandleList;
	}
};
