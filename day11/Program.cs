StreamReader reader = new(File.OpenRead("./day11/input.txt")); 

HashSet<Tuple<int,int>> galaxies = [];

int line = 0;
int maxX = 0;
int maxY = 0;
while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        for (int i = 0; i < s.Length; i++){
            if(s[i]=='#'){
                galaxies.Add(new(i,line));
                if(i > maxX) maxX = i;
                if(line > maxY) maxY = line;
            }
        }
    }
    line++;
}

HashSet<int> emptyC = [];
for (int i = 0; i <= maxX; i++){
    IEnumerable<Tuple<int, int>> column = galaxies.Where(x => x.Item1 == i);
    if(!column.Any()){
        emptyC.Add(i);
    }
}
HashSet<int> emptyR = [];
for (int i = 0; i <= maxY; i++){
    IEnumerable<Tuple<int, int>> row = galaxies.Where(x => x.Item2 == i);
    if(!row.Any()){
        emptyR.Add(i);
    }
}

List<Tuple<long,long>> expanded = [];
foreach (Tuple<int,int> galaxy in galaxies){
    int stretchX = emptyC.Where(x => x < galaxy.Item1).Count();
    int stretchY = emptyR.Where(x => x < galaxy.Item2).Count();
    expanded.Add(new(galaxy.Item1+stretchX, galaxy.Item2+stretchY));
}

long sum = 0;
for (int i = 0; i < expanded.Count; i++){
    for (int j = i+1; j < expanded.Count; j++){
        long d = dist(expanded[i], expanded[j]);
        sum += d;
    }
}
Console.WriteLine(sum);

List<Tuple<long,long>> expanded2 = [];
foreach (Tuple<int,int> galaxy in galaxies){
    int stretchX = emptyC.Where(x => x < galaxy.Item1).Count();
    int stretchY = emptyR.Where(x => x < galaxy.Item2).Count();
    expanded2.Add(new(galaxy.Item1+stretchX*999999L, galaxy.Item2+stretchY*999999L));
}

long sum2 = 0;
for (int i = 0; i < expanded2.Count; i++){
    for (int j = i+1; j < expanded2.Count; j++){
        long d = dist(expanded2[i], expanded2[j]);
        sum2 += d;
    }
}
Console.WriteLine(sum2);

static long dist(Tuple<long,long> g1, Tuple<long,long> g2){
    return Math.Abs(g1.Item1-g2.Item1) + Math.Abs(g1.Item2-g2.Item2);
}
