using Point = System.Tuple<int, int, int>;

class Brick() :IComparable<Brick>{

    public List<int> P1 {get;} = [];
    public List<int> P2 {get;} = [];

    public HashSet<Point> Blocks {get;} = [];

    public int Lowest {get;}

    public int Num {get;}

    public Brick(String s, int n): this() {
        Num = n;
        string[] s1 = s.Split('~');
        Array.ForEach(s1[0].Split(','), x => P1.Add(int.Parse(x)));
        Array.ForEach(s1[1].Split(','), x => P2.Add(int.Parse(x)));
        int minx = Math.Min(P1[0],P2[0]);
        int miny = Math.Min(P1[1],P2[1]);
        int minz = Math.Min(P1[2],P2[2]);
        int maxx = Math.Max(P1[0],P2[0]);
        int maxy = Math.Max(P1[1],P2[1]);
        int maxz = Math.Max(P1[2],P2[2]);
        for(int x = minx; x <= maxx; x++){
            for(int y = miny; y <= maxy; y++){
                for(int z = minz; z <= maxz; z++){
                    Blocks.Add(new(x,y,z));
                }
            }
        }
        Lowest = minz;
    }

    public Brick(List<int> pp1, List<int> pp2, int n): this(){
        Num = n;
        P1 = pp1;
        P2 = pp2;
        int minx = Math.Min(P1[0],P2[0]);
        int miny = Math.Min(P1[1],P2[1]);
        int minz = Math.Min(P1[2],P2[2]);
        int maxx = Math.Max(P1[0],P2[0]);
        int maxy = Math.Max(P1[1],P2[1]);
        int maxz = Math.Max(P1[2],P2[2]);
        for(int x = minx; x <= maxx; x++){
            for(int y = miny; y <= maxy; y++){
                for(int z = minz; z <= maxz; z++){
                    Blocks.Add(new(x,y,z));
                }
            }
        }
        Lowest = minz;
    }

    public Brick Lower(){
        List<int> p1 = [];
        p1 = new(P1);
        p1[2] = p1[2]-1;
        List<int> p2 = [];
        p2 = new(P2);
        p2[2] = p2[2]-1;
        return new(p1,p2, Num);
    }

    public bool Intersects(Brick other){
        foreach(Point block in Blocks){
            if(other.Blocks.Contains(block)){
                return true;
            }
        }
        return false;
    }

    public int CompareTo(Brick? other){
        if(other == null){
            throw new NotImplementedException();
        }
        return this.Lowest - other.Lowest;
    }

    public override string ToString(){
        return Num + ": " + P1[0]+","+P1[1]+","+P1[2]+"~"+P2[0]+","+P2[1]+","+P2[2];
    }
}