/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define CLUSTER_SIZE 0x400000

class MBStock
{
private:
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	uchar *Cluster;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	uchar **StockList;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	uchar *GaveList;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int StockCount;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int BlockSize;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	MBStock *Next;

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int GetBlockCount()
	{
		return CLUSTER_SIZE / this->BlockSize;
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int Block2Index(uchar *block)
	{
		errorCase(block < this->Cluster);
		errorCase(this->Cluster + CLUSTER_SIZE <= block);

		int offset = (int)((uint)block - (uint)this->Cluster);

		errorCase(offset % this->BlockSize != 0);

		return offset / this->BlockSize;
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	uchar *Index2Block(int index)
	{
		errorCase(index < 0);
		errorCase(this->GetBlockCount() <= index);

		uchar *block = this->Cluster + index * this->BlockSize;

		return block;
	}

public:
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	MBStock(int blockSize)
	{
LOG("[MBS.ctor] %d\n", blockSize);
		errorCase(blockSize < 1);
		errorCase(CLUSTER_SIZE < blockSize);
		errorCase(CLUSTER_SIZE % blockSize != 0);

		int stockCount = CLUSTER_SIZE / blockSize;

		this->Cluster = (uchar *)REAL_memAlloc(CLUSTER_SIZE);
		this->StockList = (uchar **)REAL_memAlloc(stockCount * sizeof(uchar *));
		this->GaveList = (uchar *)REAL_memAlloc(stockCount);
		this->StockCount = stockCount;
		this->BlockSize = blockSize;
		this->Next = NULL;

		for(int index = 0; index < stockCount; index++)
		{
			this->StockList[index] = this->Index2Block(index);
			this->GaveList[index] = 0;
		}
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	MBStock(const MBStock &source)
	{
		error();
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	~MBStock()
	{
		1; // 貸し出し中のブロック未回収のまま開放出来ない。memAlloc() とかどこで呼ばれるか分からない。-> 開放しない。
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void *GetBlock()
	{
		uchar *block;

		if(this->StockCount)
		{
			this->StockCount--;
			block = this->StockList[this->StockCount];
			this->GaveList[this->Block2Index(block)] = 1;
		}
		else
		{
			if(!this->Next)
				this->Next = new MBStock(this->BlockSize);

			block = (uchar *)this->Next->GetBlock();
		}
		return (void *)block;
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int IsGaveBlock(void *v_block, int tryTake)
	{
		uchar *block = (uchar *)v_block;

		if(block < this->Cluster)
			goto notGave;

		if(this->Cluster + CLUSTER_SIZE <= block)
			goto notGave;

		int index = this->Block2Index(block);

		if(this->GaveList[index] == 0) // ? 貸し出し中ではない。
			error(); // goto notGave;

		if(tryTake)
		{
			this->StockList[this->StockCount] = block;
			this->StockCount++;
			this->GaveList[this->Block2Index(block)] = 0;
		}
		return 1;

notGave:
		if(this->Next)
			return this->Next->IsGaveBlock(v_block, tryTake);

		return 0;
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int IsGaveBlock(void *v_block)
	{
		return this->IsGaveBlock(v_block, 0);
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int TryTakeBlock(void *v_block)
	{
		return this->IsGaveBlock(v_block, 1);
	}
};
