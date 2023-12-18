StreamReader reader = new(File.OpenRead("./day18/input.txt")); 

HashSet<Tuple<int,int>> trench = [];

int curX = 1;
int curY = 1;

int minX = curX;
int maxX = curX;
int minY = curY;
int maxY = curY;

while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        string[] split = s.Split(" ");
        string dir = split[0];
        int dist = int.Parse(split[1]);
        if(dir=="U"){
            for (int i = 1; i <= dist; i++){
                trench.Add(new(curX,curY-i));
            }
            curY = curY-dist;
            if(curY < minY) minY = curY;
        }else if(dir=="D"){
            for (int i = 1; i <= dist; i++){
                trench.Add(new(curX,curY+i));
            }
            curY = curY+dist;
            if(curY > maxY) maxY = curY;
        }else if(dir=="L"){
            for (int i = 1; i <= dist; i++){
                trench.Add(new(curX-i,curY));
            }
            curX = curX-dist;
            if(curX < minX) minX = curX;
        }else if(dir=="R"){
            for (int i = 1; i <= dist; i++){
                trench.Add(new(curX+i,curY));
            }
            curX = curX+dist;
            if(curX > maxX) maxX = curX;
        }else{
            throw new("nope");
        }
    }
}

for(int y = minY; y <= maxY; y++){
    for(int x = minX; x <= maxX; x++){
        if(trench.Contains(new(x,y))){
            Console.Write('#');
        }else{
            Console.Write('.');
        }
    }
    Console.WriteLine();
}