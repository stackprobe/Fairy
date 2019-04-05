	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
class finalizers
{
private:
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	autoList<void (*)(void)> *Finalizers;

public:
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	finalizers()
	{
		this->Finalizers = new autoList<void (*)(void)>();
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	finalizers(const finalizers &source)
	{
		error();
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	~finalizers()
	{
		delete this->Finalizers;
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void AddFunc(void (*func)(void))
	{
		errorCase(!func);
		this->Finalizers->AddElement(func);
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void RemoveFunc(void (*func)(void))
	{
		for(int index = this->Finalizers->GetCount() - 1; 0 <= index; index--) // LIFO
		{
			if(this->Finalizers->GetElement(index) == func)
			{
				this->Finalizers->DesertElement(index);
				break;
			}
		}
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void Flush(void)
	{
		while(this->Finalizers->GetCount()) // LIFO
		{
			this->Finalizers->UnaddElement()();
		}
	}
};
