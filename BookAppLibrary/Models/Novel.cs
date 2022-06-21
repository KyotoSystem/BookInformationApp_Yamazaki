using BookAppLibrary.Enums;

namespace BookAppLibrary.Models
{
    public class Novel : Book
    {
        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">コード</param>
        public Novel(string code) : base(code,SpeciesType.Novel)
        {
           
        }
        #endregion

        #region プロパティ
        ///<summary>
        ///作者
        ///</summary>
        public string Author { get; set; }

        #endregion

        #region メソッド
        /// <summary>
        /// 種別名を取得する。
        /// </summary>
        /// <returns>種別名</returns>
        public override string GetSpecies()
        {
            return "小説";
        }
        #endregion
    }
}
