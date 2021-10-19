namespace AnalysisOfChessState
{
    public static class GameStates
    {
        public static StateCode Start = new StateCode("07br17bk27bb37bK47bQ57bb67bk77br" +
                                                      "06bp16bp26bp36bp46bp56bp66bp76bp" +
                                                      "01wp11wp21wp31wp41wp51wp61wp71wp" +
                                                      "00wr10wk20wb30wK40wQ50wb60wk70wr");

        public static StateCode BugState = new StateCode("00wr01wp03bb06bp07br10wk12wp16bp" +
                                                         "17bk20wb23wp26bp21wK31wp35bp37bK" +
                                                         "40wQ41wp46bp47bQ50wb51wp52wk56bp" +
                                                         "57bb61wp66bp67bk70wr71wp76bp77br");
    }
}