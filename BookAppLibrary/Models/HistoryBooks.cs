using System.Collections.Generic;
using BookAppLibrary.Enums;

namespace BookAppLibrary.Models
{
    public class HistoryBooks : Book
    {
        #region　コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">コード</param>
        public HistoryBooks(string code) : base(code , SpeciesType.HistoryBooks) { }

        #endregion


        #region　プロパティ
        /// <summary>
        /// 歴史表記
        /// </summary>
        public string HistoricalNotation { get; set; }
        /// <summary>
        /// 人物
        /// </summary>
        public List<string> Figure { get; set; }
        /// <summary>
        /// 監修
        /// </summary>
        public string Supervision { get; set; }


        #endregion

        #region　メソッド
        /// <summary>
        /// 種別名を取得
        /// </summary>
        /// <returns>種別名</returns>
        public override string GetSpecies()
        {
            return "歴史本";
        }

        #endregion
    }
}
