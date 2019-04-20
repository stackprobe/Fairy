/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define DEAD_COUNT_MAX 1024

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
template <class Element_t>
class autoQueue
{
private:
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	autoList<Element_t> *List;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int TopIndex;

public:
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	autoQueue()
	{
		this->List = new autoList<Element_t>();
		this->TopIndex = 0;
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	autoQueue(const autoQueue &source)
	{
		error();
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	~autoQueue()
	{
		delete this->List;
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void Clear()
	{
		this->List->Clear();
		this->TopIndex = 0;
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void Trim()
	{
		if(1 <= this->TopIndex)
		{
			this->List->RemoveElements(0, this->TopIndex);
			this->TopIndex = 0;
		}
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void Clear(void (*func)(Element_t))
	{
		this->Trim();
		this->List->Clear(func);
		this->TopIndex = 0;
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void Enqueue(Element_t e)
	{
		this->List->AddElement(e);
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void Enqueue(Element_t e, int repeat_num)
	{
		for(int c = 0; c < repeat_num; c++)
		{
			this->Enqueue(e);
		}
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	Element_t Dequeue()
	{
		Element_t e = this->List->GetElement(this->TopIndex);

		this->TopIndex++;

		if(this->TopIndex == this->List->GetCount())
		{
			this->List->Clear();
			this->TopIndex = 0;
		}
		else if(DEAD_COUNT_MAX < this->TopIndex)
		{
			this->List->RemoveElements(0, this->TopIndex);
			this->TopIndex = 0;
		}
		return e;
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	Element_t Dequeue(Element_t default_e)
	{
		if(this->TopIndex < this->List->GetCount())
		{
			return this->Dequeue();
		}
		return default_e;
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	autoList<Element_t> *GetList_DIRECT()
	{
		return this->List;
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int GetTopIndex_DIRECT()
	{
		return this->TopIndex;
	}
};

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
template <class Element_t>
void releaseQueue(autoQueue<Element_t> *queue, void (*func)(Element_t e))
{
	errorCase(!queue);
	errorCase(!func);

	queue->Trim();
	queue->GetList_DIRECT()->CallAllElement(func);
	delete queue;
}
