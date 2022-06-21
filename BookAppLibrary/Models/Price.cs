using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAppLibrary.Models
{
    public class Price
    {
        #region　コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">コード</param>
        public Price(string code)
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
        /// 価格
        /// </summary>
        public int Prices { get; set; }
        #endregion

        #region　メソッド
        /// <summary>
        /// 消費税込みの価格を取得
        /// </summary>
        /// <returns>価格</returns>
        public virtual int GetPrice()
        {
            return (int)(Prices * 1.1D);
        }
        #endregion
    }
}
