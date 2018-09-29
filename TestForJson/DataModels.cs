using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestForJson
{
    /// <summary>
    /// 根据协议当中的描述，收到的数据可以看做一个二维表格，成为槽"Slot"和格子"Cell"，每个Cell两字节   19*9*2
    /// 使用具有实际意义的量来定位某一数据
    /// </summary>
    public class PiccoItem
    {
        /// <summary>
        /// 纵向的列,base of 0
        /// </summary>
        public int Slot { set; get; }
        /// <summary>
        /// 横向的行,base of 0 ,Max reache 8
        /// </summary>
        public int Cell { set; get; }
        /// <summary>
        /// 表征占用cell的前半部分还是后半部分
        /// </summary>
        public InCellType InCellType { set; get; }
        public string ItemName { set; get; }
        public string Unit { set; get; }
        public float Factor { set; get; }

        public PiccoItem()
        {

        }

        public PiccoItem(int slot, int cell, InCellType inCellType, string itemName, string unit, float factor)
        {
            Slot = slot;
            Cell = cell;
            InCellType = inCellType;
            ItemName = itemName;
            Unit = unit;
            Factor = factor;
        }
    }

    public enum InCellType
    {
        First = 1,
        Second = 2,
        Both = 0
    }

    public class PiccoList
    {
        public List<PiccoItem> PiccoItemList { set; get; }

    }
}
