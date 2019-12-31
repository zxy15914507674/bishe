/****
 * 
 *  干涉矩阵
 *  
 * 如矩阵： 
 *         1 0 0
 *         0 1 0
 *         0 0 0
 * 
 * 就有第三行全为0，满足装配，对应的仪器部件可以装配，
 * 然后移除第三行和第三列的元素，继续判断某一列是否全部为0
 * 
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace InterferenceMatrixPro
{
    public class InterferenceMatrix
    {

        List<Note> listNote = new List<Note>();
        //存储行号
        List<int> listRowIndex = new List<int>();
        //已经移除了的行号集合
        public List<int> hasRemovedRowIndex = new List<int>();
        public InterferenceMatrix(Dictionary<int, List<int>> dicMatrix)
        {
            if(dicMatrix==null){
                return;
            }   
            foreach (int row in dicMatrix.Keys)
            {

                for (int column =0; column < dicMatrix[row].Count;column++)
                {
                    listNote.Add(new Note() { 
                         ColumnIndex=column+1,
                         RowIndex=row,
                         value=dicMatrix[row][column]
                    });
                }

                listRowIndex.Add(row);
            }
        }



        /// <summary>
        /// 检查当前行是否满足约束
        /// </summary>
        /// <param name="index">当前行号</param>
        /// <returns></returns>
        public bool CheckConstraint(int index) {
            
            //检查当前行是否全部为0
            foreach (Note note in listNote)
            {
                 if(note.RowIndex==index){
                    if(note.value!=0){
                        return false;
                    }
                 }
            }
            return true;
        }


        /// <summary>
        /// 检查所有满足约束的行，返回行号集合
        /// </summary>
        /// <returns></returns>
        public List<int> CheckAllRowMeetConstraint() {

            List<int> listRow = new List<int>();

            foreach (int  rowIndex in listRowIndex)
            {
                if(CheckConstraint(rowIndex)){
                    listRow.Add(rowIndex);
                }
            }
            return listRow;
        }



        /// <summary>
        /// 移除矩阵index行和index列中的元素
        /// </summary>
        /// <param name="index"></param>
        public void RemoveElement(int index) {
            if (!listRowIndex.Contains(index))
            {
                return;
            }

            for (int noteIndex = listNote.Count - 1;noteIndex>=0; noteIndex--)
            {
                if (listNote[noteIndex].RowIndex == index || listNote[noteIndex].ColumnIndex == index)
                {
                    
                    listNote.Remove(listNote[noteIndex]);
                    
                }
            }

            listRowIndex.Remove(index);

            //把当前行号添加进已移除的行号集合中
            hasRemovedRowIndex.Add(index);
        }


        public int NoteCount() {
            return listNote.Count;
        }

        public void Print()
        {
            foreach (Note note in listNote)
            {
                Console.WriteLine("note.RowIndex: " + note.RowIndex + " note.ColumnIndex: " + note.ColumnIndex + " note.value: " + note.value);
            }
        }
    }


    public struct Note { 
        public int RowIndex;

        public int ColumnIndex;

        public int value;
    }





}
