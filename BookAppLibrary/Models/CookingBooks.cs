using BookAppLibrary.Enums;

namespace BookAppLibrary.Models
{
    
    public class CookingBooks : Book
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">コード</param>
        public CookingBooks(string code) : base (code, SpeciesType.CookingBooks) { }
        #endregion

        #region プロパティ
        /// <summary>
        /// ジャンル
        /// </summary>
        public string Genre { get; set; }

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
            return "料理本";
        }
        #endregion
    }
}
