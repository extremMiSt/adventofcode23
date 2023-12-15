StreamReader reader = new(File.OpenRead("./day15/input.txt")); 
string? s = reader.ReadLine();
if(s == null){
    Environment.Exit(-1);
}
string[] line = s.Split(',');

int sum = 0;
foreach(string token in line){
    sum += Hash(token);
}
Console.WriteLine("part 1:" + sum);

List<Tuple<string, int>>[] map = new List<Tuple<string, int>>[256];
for (int i = 0; i < 256; i++){
    map[i] = [];
}
foreach(string token in line){
    if(token.Contains('=')){
        string label = token[..token.IndexOf('=')];
        string lens = token[(token.IndexOf('=')+1)..];
        int box = Hash(label);
        IEnumerable<Tuple<string, int>> en = map[box].Where(x => x.Item1 == label);
        if (en.Any()){
            Tuple<string, int> tup = en.First();
            map[box][map[box].IndexOf(tup)] = new(label,int.Parse(lens));
        }else{
            map[box].Add(new(label,int.Parse(lens)));
        }
    }else{
        string label = token[..^1];
        int box = Hash(label);
        map[box] = map[box].Where(x => x.Item1 != label).ToList();
    }
}
long sum2=0;
for (int i = 0; i < 256; i++){
    for(int j =0; j< map[i].Count; j++){
        long p = (i+1)*(j+1)* map[i][j].Item2;
        sum2+= p;
    }
}
Console.WriteLine("part 2:" + sum2);

static int Hash(string s){
    int cur = 0;
    foreach(char c in s){
        cur = (cur + c)*17 % 256;
    }
    return cur;
}