namespace AlgebraSystem.Parsing
{
    public enum Symbol
    {
        NULL,
        Word, Number, String,
        Add, Subtract, Multiply, Divide, Power,
        OpenRoundBracket, CloseRoundBracket, OpenSquareBracket, CloseSquareBracket, OpenCurlyBracket, CloseCurlyBracket,
        Dot, Comma, Colon, SemiColon, Assign, Less, Greater, Increase, Decrease,
        AddTo, SubtractTo, MultiplyTo, DivideTo, PowerTo,
        Equal, LessEqual, GreaterEqual, NotEqual, Or, And
    }
}
