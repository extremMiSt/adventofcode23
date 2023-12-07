class Hand(string cards, long bid){

    public string Cards { get; } = cards;
    public long Bid { get; } = bid;

    public override string ToString(){
        return "[" + Cards + ", " + Bid + "]";
    }
}