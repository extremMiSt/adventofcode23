StreamReader reader = new(File.OpenRead("./day14/input.txt")); 
HashSet<Tuple<int,int>> round = [];
HashSet<Tuple<int,int>> square = [];

int size = 100;

int line = 0;
while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        for (int i = 0; i < s.Length; i++){
            if(s[i]=='#'){
                square.Add(new(i, line));
            }else if(s[i]=='O'){
                round.Add(new(i, line));
            }
        }
    }
    line++;
}

HashSet<Tuple<int,int>> north = North(round,square);

int sum = 0;
foreach(Tuple<int,int> stone in north){
    sum += size - stone.Item2;
}

Console.WriteLine("Teil 1: " + sum);

HashSet<Tuple<int,int>> spun = round;

for (int i = 0; i < 1000; i++){
    int sum2 = 0;
    spun = Spin(spun, square, size);
    foreach(Tuple<int,int> stone in spun){
        sum2 += size - stone.Item2;
    }
    Console.Write(sum2 + " ");
}
//104416 104346 104527 104680 104794 104915 105101 105343 105566 105791 106062 106348 106626 106912 107194 107444 107693 107930 108116 108372 108597 108831 109095 109379 109633 109921 110192 110410 110650 110917 111187 111459 111718 111948 112154 112380 112597 112834 113048 113271 113461 113657 113826 113982 114157 114348 114532 114722 114934 115124 115301 115483 115655 115814 115966 116127 116329 116480 116642 116810 116986 117156 117312 117435 117561 117670 117764 117869 117933 117991 118051 118136 118204 118266 118327 118371 118429 118450 118508 118542 118586 118607 118647 118664 118674 118700 118713 118759 118763 118759 118752 118784 118774 118756 118768 118759 118773 118766 118767 118741 118754 118774 118779 118774 118762 118785 118752 118737 118741 118777 118780 118782 118786 118748 118755 118752 118760 
//118747 118780 118792 118768 118756 118748 118778 118758 118763 118759 118766 118762 118768 118779 118754 118781 118770 118749 118729 118766 118785 118774 118782 118766 118767 118740 118749 118752 118772 118788 118786 118768 118736 118767 118763 118755 118755 118784 118774 118756 118768 118759 118773 118766 118767 118741 118754 118774 118779 118774 118762 118785 118752 118737 118741 118777 118780 118782 118786 118748 118755 118752 118760 

//117 in prelude
//63 in loop

Console.WriteLine((1000000000-118)%64);
//i probably did guess the right element? this formula seems crap and gives the wrong result.


static HashSet<Tuple<int,int>> North(HashSet<Tuple<int,int>> round, HashSet<Tuple<int,int>> square){
    HashSet<Tuple<int,int>> north = [];
    foreach(Tuple<int,int> stone in round){
        for(int i = stone.Item2; i>= 0; i--){
            if(square.Contains(new(stone.Item1,i))){
                int n = i+1;
                while(north.Contains(new(stone.Item1,n))){
                    n++;
                }
                north.Add(new(stone.Item1,n));
                break;
            }else if(i==0){
                int n = i;
                while(north.Contains(new(stone.Item1,n))){
                    n++;
                }
                north.Add(new(stone.Item1,n));
                break;
            }
        }
    }
    return north;
}

static HashSet<Tuple<int,int>> South(HashSet<Tuple<int,int>> round, HashSet<Tuple<int,int>> square, int size){
    HashSet<Tuple<int,int>> result = [];
    foreach(Tuple<int,int> stone in round){
        for(int i = stone.Item2; i < size; i++){
            if(square.Contains(new(stone.Item1,i))){
                int n = i-1;
                while(result.Contains(new(stone.Item1,n))){
                    n--;
                }
                result.Add(new(stone.Item1,n));
                break;
            }else if(i==size-1){
                int n = i;
                while(result.Contains(new(stone.Item1,n))){
                    n--;
                }
                result.Add(new(stone.Item1,n));
                break;
            }
        }
    }
    return result;
}

static HashSet<Tuple<int,int>> East(HashSet<Tuple<int,int>> round, HashSet<Tuple<int,int>> square, int size){
    HashSet<Tuple<int,int>> result = [];
    foreach(Tuple<int,int> stone in round){
        for(int i = stone.Item1; i < size; i++){
            if(square.Contains(new(i,stone.Item2))){
                int n = i-1;
                while(result.Contains(new(n,stone.Item2))){
                    n--;
                }
                result.Add(new(n, stone.Item2));
                break;
            }else if(i==size-1){
                int n = i;
                while(result.Contains(new(n,stone.Item2))){
                    n--;
                }
                result.Add(new(n,stone.Item2));
                break;
            }
        }
    }
    return result;
}

static HashSet<Tuple<int,int>> West(HashSet<Tuple<int,int>> round, HashSet<Tuple<int,int>> square, int size){
    HashSet<Tuple<int,int>> result = [];
    foreach(Tuple<int,int> stone in round){
        for(int i = stone.Item1; i >=0; i--){
            if(square.Contains(new(i,stone.Item2))){
                int n = i+1;
                while(result.Contains(new(n,stone.Item2))){
                    n++;
                }
                result.Add(new(n, stone.Item2));
                break;
            }else if(i==0){
                int n = i;
                while(result.Contains(new(n,stone.Item2))){
                    n++;
                }
                result.Add(new(n,stone.Item2));
                break;
            }
        }
    }
    return result;
}

static HashSet<Tuple<int,int>> Spin(HashSet<Tuple<int,int>> round, HashSet<Tuple<int,int>> square, int size){
    HashSet<Tuple<int,int>> n = North(round, square);
    HashSet<Tuple<int,int>> w = West(n, square, size);
    HashSet<Tuple<int,int>> s = South(w, square, size);
    HashSet<Tuple<int,int>> e = East(s, square, size);
    return e;
}
