public class UsefulChessCodes
{
    public static ChessCode StartChessState { get; } = new ChessCode
    (
        "0;0;wrt_1;0;wnt_2;0;wbt_3;0;wqt_4;0;wkt_5;0;wbt_6;0;wnt_7;0;wrt_" +
        "0;1;wpt_1;1;wpt_2;1;wpt_3;1;wpt_4;1;wpt_5;1;wpt_6;1;wpt_7;1;wpt_" +
        "0;7;brt_1;7;bnt_2;7;bbt_3;7;bqt_4;7;bkt_5;7;bbt_6;7;bnt_7;7;brt_" +
        "0;6;bpt_1;6;bpt_2;6;bpt_3;6;bpt_4;6;bpt_5;6;bpt_6;6;bpt_7;6;bpt_" +
        "/8;8/w"
    );
    
    public static ChessCode PawnPromotionHelp { get; } = new ChessCode
    (
        "0;7;wkt_0;0;bkt_6;6;wpt_"
    );
}