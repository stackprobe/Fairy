/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
// ---- FontFile ----

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void AddFontFile(int etcId, char *localFile);

// ---- FontHandle ----

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct FontHandle_st
{
	int Handle;
	char *FontName;
	int FontSize;
	int FontThick;
	int AntiAliasing;
	int EdgeSize;
	int ItalicFlag;
}
FontHandle_t;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
FontHandle_t *CreateFontHandle(char *fontName, int fontSize, int fontThick = 6, int antiAliasing = 1, int edgeSize = 0, int italicFlag = 0);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void ReleaseFontHandle(FontHandle_t *fh);

// <-- cdtor

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
FontHandle_t *GetFontHandle(char *fontName, int fontSize, int fontThick = 6, int antiAliasing = 1, int edgeSize = 0, int italicFlag = 0);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void ReleaseAllFontHandle(void);

// ----

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawStringByFont(int x, int y, char *str, FontHandle_t *fh, int tategakiFlag = 0, int color = GetColor(255, 255, 255), int edgeColor = GetColor(0, 0, 0));
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int GetDrawStringByFontWidth(char *str, FontHandle_t *fh, int tategakiFlag = 0);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawStringByFont_XCenter(int x, int y, char *str, FontHandle_t *fh, int tategakiFlag = 0, int color = GetColor(255, 255, 255), int edgeColor = GetColor(0, 0, 0));
