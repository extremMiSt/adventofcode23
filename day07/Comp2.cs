class Comp2 : IComparer<Hand>{
    
    private readonly char[] val = ['J','2','3','4','5','6','7','8','9','T','Q','K','A'];  
    public int Compare(Hand? x, Hand? y){
        if(y is not null && x is not null){
            if(Category(x.Cards) != Category(y.Cards)){
                return Category(x.Cards) - Category(y.Cards);
            }else{
                for (int i = 0; i < x.Cards.Length; i++){
                    if(x.Cards[i]==y.Cards[i]){
                        continue;
                    }else{
                        return Array.IndexOf(val, x.Cards[i]) - Array.IndexOf(val, y.Cards[i]);
                    }
                }
            }
        }
        return 0;
    }

    
    public int Category(string Cards){
        Dictionary<char, int> map = [];
        for (int i = 0; i < Cards.Length; i++){
            if(map.ContainsKey(Cards[i])){
                map[Cards[i]] += 1;
            }else{
                map[Cards[i]] = 1;
            }
            
        }

        bool seen5 =false;
        bool seen4 =false;
        bool seen3 =false;
        bool seen2 =false;
        bool seen2twice =false;

        int jokers = 0;
        if(map.ContainsKey('J')){
            jokers = map['J'];
        }

        foreach(char c in map.Keys){
            if(c=='J') continue;

            seen5 = seen5 || map[c]==5;
            seen4 = seen4 || map[c]==4;
            seen3 = seen3 || map[c]==3;
            if(seen2){
                seen2twice = seen2twice || map[c]==2;
            }
            seen2 = seen2 || map[c]==2;
        }

        if(seen5 || seen4 && jokers==1 || seen3 && jokers==2 || seen2 && jokers==3 || jokers==5 || jokers==4){
            return 7;
        }
        if(seen4 && jokers==0 || seen3 && jokers==1 && !seen2 || seen2 && jokers==2 || jokers==3 && !seen2){
            return 6;
        }
        if(seen3 && seen2 || seen2 && seen2twice && jokers==1){
            return 5;
        }
        if(seen3 && !seen2 || seen2 && jokers==1 && !seen2twice || jokers==2 && !seen2 && !seen3){
            return 4;
        }
        if(seen2 && seen2twice && jokers==0){
            return 3;
        }
        if(seen2 || jokers==1){
            return 2;
        }
        return 1;

    }
}