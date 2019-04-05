/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct SubScreen_st
{
	int W;
	int H;
	int AFlag;
	int Handle;
}
SubScreen_t;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
SubScreen_t *CreateSubScreen(int w, int h, int aFlag = 0);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void ReleaseSubScreen(SubScreen_t *i);

// <-- cdtor

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int GetHandle(SubScreen_t *i);

// <-- accessor

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
extern int CurrDrawScreenHandle;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void ChangeDrawScreen(int handle);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void ChangeDrawScreen(SubScreen_t *screen);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void RestoreDrawScreen(void);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
i2D_t GetSubScreenSize(SubScreen_t *screen);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
i2D_t GetDrawScreenSize(void);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void UnloadAllSubScreenHandle(void);
