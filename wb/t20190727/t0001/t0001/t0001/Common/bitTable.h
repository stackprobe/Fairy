	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
class bitTable
{
private:
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	bitList *List;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int Width;

public:
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	bitTable(int w)
	{
		errorCase(w < 1 || IMAX < w);

		this->List = new bitList();
		this->Width = w;
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	bitTable(const bitTable &source)
	{
		error();
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	~bitTable()
	{
		delete this->List;
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void Clear()
	{
		this->List->Clear();
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void PutBit(int x, int y, int value)
	{
		errorCase(x < 0);
		errorCase(y < 0);
		errorCase(this->Width <= x);
		errorCase(IMAX / this->Width <= y);

		return this->List->PutBit(x + y * this->Width, value);
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int RefBit(int x, int y)
	{
		errorCase(x < 0);
		errorCase(y < 0);
		errorCase(this->Width <= x);
		errorCase(IMAX / this->Width <= y);

		return this->List->RefBit(x + y * this->Width);
	}
};
