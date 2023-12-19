class Rule(string k, int p, bool l, string n, bool d) {
    readonly public string Key = k;
    readonly public int Param = p;
    readonly public bool Less = l;
    readonly public string Next = n;
    readonly public bool Default = d;

    public bool Matches(Dictionary<string,int> d){
        if(Default){
            return true;
        }
        if(Less){
            return d[Key] < Param;
        }else{
            return d[Key] > Param;
        }
    }
}