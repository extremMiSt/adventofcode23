StreamReader reader = new(File.OpenRead("./day16/input.txt")); 

Dictionary<Tuple<int,int>,char> map = [];

int mX = 0;
int mY = 0;
while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        mX = s.Length;
        for (int x = 0; x < s.Length; x++){
            if(s[x]!='.'){
                map.Add(new(x,mY), s[x]);
            }
        }
    }
    mY++;
}

Console.WriteLine("part1: " + doit(map, mX, mY, new(new(0,0),'>')));
int max = 0;
for(int y = 0; y < mY; y++){
    max = Math.Max(max, doit(map, mX, mY, new(new(0,y),'>')));
    max = Math.Max(max, doit(map, mX, mY, new(new(mX-1,y),'<')));
}
for(int x = 0; x < mX; x++){
    max = Math.Max(max, doit(map, mX, mY, new(new(x,0),'v')));
    max = Math.Max(max, doit(map, mX, mY, new(new(x,mY-1),'^')));
}
Console.WriteLine("part2: " + max);


static int doit(Dictionary<Tuple<int,int>,char> map, int mX, int mY, Tuple<Tuple<int,int>,char> start){
    HashSet<Tuple<Tuple<int,int>,char>> done = [];
    Queue<Tuple<Tuple<int,int>,char>> front = [];
    front.Enqueue(start);

    while(front.Count != 0){
        //check if we are retracing an already trodden path, or left the map.
        Tuple<Tuple<int,int>,char> cur = front.Dequeue();
        Tuple<int,int> pos = cur.Item1;
        if(done.Contains(cur) || pos.Item1<0 || pos.Item2<0 || pos.Item1 >= mX || pos.Item2 >= mY){
            continue;
        }

        //progress on current path
        done.Add(cur);
        if(!map.ContainsKey(pos)){
            if(cur.Item2=='>'){
                front.Enqueue(new(new(pos.Item1+1, pos.Item2), cur.Item2));
            }else if(cur.Item2=='<'){
                front.Enqueue(new(new(pos.Item1-1, pos.Item2), cur.Item2));
            }else if(cur.Item2=='^'){
                front.Enqueue(new(new(pos.Item1, pos.Item2-1), cur.Item2));
            }else if(cur.Item2=='v'){
                front.Enqueue(new(new(pos.Item1, pos.Item2+1), cur.Item2));
            }
        }else if(map[pos]=='\\'){
            if(cur.Item2=='>'){
                front.Enqueue(new(new(pos.Item1, pos.Item2+1), 'v'));
            }else if(cur.Item2=='<'){
                front.Enqueue(new(new(pos.Item1, pos.Item2-1), '^'));
            }else if(cur.Item2=='^'){
                front.Enqueue(new(new(pos.Item1-1, pos.Item2), '<'));
            }else if(cur.Item2=='v'){
                front.Enqueue(new(new(pos.Item1+1, pos.Item2), '>'));
            }
        }else if(map[pos]=='/'){
            if(cur.Item2=='>'){
                front.Enqueue(new(new(pos.Item1, pos.Item2-1), '^'));
            }else if(cur.Item2=='<'){
                front.Enqueue(new(new(pos.Item1, pos.Item2+1), 'v'));
            }else if(cur.Item2=='^'){
                front.Enqueue(new(new(pos.Item1+1, pos.Item2), '>'));
            }else if(cur.Item2=='v'){
                front.Enqueue(new(new(pos.Item1-1, pos.Item2), '<'));
            }
        }else if(map[pos]=='-'){
            if(cur.Item2=='>'){
                front.Enqueue(new(new(pos.Item1+1, pos.Item2), cur.Item2));
            }else if(cur.Item2=='<'){
                front.Enqueue(new(new(pos.Item1-1, pos.Item2), cur.Item2));
            }else if(cur.Item2=='^'){
                front.Enqueue(new(new(pos.Item1+1, pos.Item2), '>'));
                front.Enqueue(new(new(pos.Item1-1, pos.Item2), '<'));
            }else if(cur.Item2=='v'){
                front.Enqueue(new(new(pos.Item1+1, pos.Item2), '>'));
                front.Enqueue(new(new(pos.Item1-1, pos.Item2), '<'));
            }
        }else if(map[pos]=='|'){
            if(cur.Item2=='>'){
                front.Enqueue(new(new(pos.Item1, pos.Item2-1), '^'));
                front.Enqueue(new(new(pos.Item1, pos.Item2+1), 'v'));
            }else if(cur.Item2=='<'){
                front.Enqueue(new(new(pos.Item1, pos.Item2-1), '^'));
                front.Enqueue(new(new(pos.Item1, pos.Item2+1), 'v'));
            }else if(cur.Item2=='^'){
                front.Enqueue(new(new(pos.Item1, pos.Item2-1), cur.Item2));
            }else if(cur.Item2=='v'){
                front.Enqueue(new(new(pos.Item1, pos.Item2+1), cur.Item2));
            }
        }
    }
    HashSet<Tuple<int,int>> filtered = new(done.Select(x => x.Item1));
    return filtered.Count;
}
