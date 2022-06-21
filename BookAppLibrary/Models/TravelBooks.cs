using BookAppLibrary.Enums;

namespace BookAppLibrary.Models
{
    public class TravelBooks : Book
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">コード</param>
        public TravelBooks(string code) : base(code , SpeciesType.TravelBooks) { }

        #endregion

        #region プロパティ

        /// <summary>
        /// 旅行表記
        /// </summary>
        public string TravelNotation { get; set; }

        /// <summary>
        /// 監修
        /// </summary>
        public string Supervision { get; set; }

        #endregion

        #region メソッド

        /// <summary>
        /// 種別名を取得
        /// </summary>
        /// <returns>種別名</returns>
        public override string GetSpecies()
        {
            return "旅行本";
        }
        #endregion
    }
}
