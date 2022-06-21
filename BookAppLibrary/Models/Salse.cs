using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookAppLibrary.Enums;

namespace BookAppLibrary.Models
{
    public class Salse
    {
        #region　コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">コード</param>
        public Salse(string code)
        {
            Code = code;
        }
        #endregion

        #region　プロパティ
        /// <summary>
        /// 書籍コード
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 売上金額
        /// </summary>
        public int SalesAmount { get; set; }
        #endregion

    }
}
