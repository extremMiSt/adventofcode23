List<String> map = [];

StreamReader reader = new(File.OpenRead("./day13/input.txt")); 

long sum = 0;
long sum2 = 0;
while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        if(s.Equals("")){
            for(int cand = 0; cand < map[0].Length-1; cand++){
                int miss = 0;
                for(int i = 0; (cand-i)>=0 && (cand+i+1)<map[0].Length; i++){
                    for(int y = 0; y<map.Count; y++){
                        miss += map[y][cand-i] == map[y][cand+i+1] ? 0:1;
                    }
                }
                if(miss == 0){
                    sum += cand+1;
                }
                if(miss == 1){
                    sum2 += cand+1;
                }
            }

            for(int cand = 0; cand < map.Count-1; cand++){
                int miss = 0;
                for(int i = 0; (cand-i)>=0 && (cand+i+1)<map.Count; i++){
                    for(int x = 0; x<map[cand-i].Length; x++){
                        miss += map[cand-i][x] == map[cand+i+1][x] ? 0:1;
                    }
                }
                if(miss == 0){
                    sum += (cand+1)*100;
                }
                if(miss == 1){
                    sum2 += (cand+1)*100;
                }
            }
            map = [];
        }else{
            map.Add(s);
        }
    }
}
Console.WriteLine(sum);
Console.WriteLine(sum2);
