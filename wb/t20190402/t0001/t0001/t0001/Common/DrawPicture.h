/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct DrawPicExtra_st
{
	taskList *TL;
	resCluster<PicInfo_t *> *PicRes;
	int BlendInvOn;
	int MosaicOn;
	int IntPosOn;
	int IgnoreError;

	// wrapped by DPE_* -->

	double A;
	int AlphaOn;
	int BlendAddOn;
	double R;
	double G;
	double B;
	int BrightOn;
	int GraphicHandleFlag;
	i2D_t GraphicSize; // GraphicHandleFlag != 0 ÇÃÇ∆Ç´ DrawCenter(), DrawBegin() Ç≈ïKóvÅB
}
DrawPicExtra_t;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
extern DrawPicExtra_t DPE;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawFree(int picId, double ltx, double lty, double rtx, double rty, double rbx, double rby, double lbx, double lby);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawRect_LTRB(int picId, double l, double t, double r, double b);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawRect(int picId, double l, double t, double w, double h);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawSimple(int picId, double x, double y);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawCenter(int picId, double x, double y);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawBegin(int picId, double x, double y);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawSlide(double x, double y);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawRotate(double rot);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawZoom_X(double z);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawZoom_Y(double z);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawZoom(double z);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawEnd(void);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DPE_SetAlpha(double a);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DPE_SetBlendAdd(double a);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DPE_SetBright(double cR, double cG, double cB);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DPE_SetBright(int color);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DPE_SetGraphicSize(i2D_t size);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DPE_Reset(void);
