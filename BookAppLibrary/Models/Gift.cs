using BookAppLibrary.Enums;

namespace BookAppLibrary.Models
{
    public class Gift : Book
    {
        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">コード</param>
        public Gift(string code) : base(code, SpeciesType.Gift) { }

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
            return "景品";
        }

        #endregion
    }
}
