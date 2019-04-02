/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
enum
{
	DATSTR_APPNAME,
	DATSTR_AUTHOR,
	DATSTR_COPYRIGHT,
	DATSTR_PCT_S_SPC_PCT_S,

	DATSTR_MAX, // num of member
};

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *GetDatString(int datStrId);
