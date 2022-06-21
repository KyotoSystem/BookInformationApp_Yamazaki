using System.Collections.Generic;
using BookAppLibrary.Enums;

namespace BookAppLibrary.Models
{
    public class GravurePhotoBooks : Book
    {
        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">コード</param>
        public GravurePhotoBooks(string code) : base(code, SpeciesType.GravurePhotoBooks) { }

        #endregion

        #region　プロパティ

        /// <summary>
        /// 撮影者
        /// </summary>
        public string PhotoGrapher { get; set; }

        /// <summary>
        /// 出演
        /// </summary>
        public List<string> Performance { get; set; }

        /// <summary>
        /// 監修
        /// </summary>
        public string Supervision { get; set;}

        #endregion

        #region　メソッド

        /// <summary>
        /// 種別名を取得
        /// </summary>
        /// <returns>種別名</returns>
        public override string GetSpecies()
        {
            return "グラビア写真集";
        }

        #endregion
    }
}
