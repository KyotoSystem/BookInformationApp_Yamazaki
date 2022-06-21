using BookAppLibrary.Enums;

namespace BookAppLibrary.Models
{
    public class Other : Book
    {
        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">コード</param>
        public Other(string code) :base(code, SpeciesType.Other) { }

        #endregion

        #region　プロパティ

        /// <summary>
        /// ジャンル
        /// </summary>
        public string Genre { get; set; }

        #endregion

        #region　メソッド

        /// <summary>
        /// 種別名を取得
        /// </summary>
        /// <returns>種別名</returns>
        public override string GetSpecies()
        {
            return "その他";
        }

        #endregion
    }
}
