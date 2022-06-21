using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAppLibrary.Models
{
    public class Sold
    {
        #region　コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">コード</param>
        public Sold(string code)
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
        /// 販売個数
        /// </summary>
        public int QuantitySold { get; set; }
        #endregion
    }
}
