using BookAppLibrary.Enums;

namespace BookAppLibrary.Models
{
    public class Dictionary : Book
    {
        #region コンストラクタ
        ///<summary>
        ///コンストラクタ
        /// </summary>
        /// <param name="code">コード</param>
        public Dictionary(string code) : base(code, SpeciesType.Dictionary) { }
        #endregion

        #region プロパティ
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
            return "辞書";
        }
        #endregion

    }
}
