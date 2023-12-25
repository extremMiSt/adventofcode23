namespace DictionaryHelper{

    public static class DictionaryHelper{

        static public void AddCreate<K,V>(this Dictionary<K,HashSet<V>> dict, K key, V elem) where K : notnull{
            if(dict.ContainsKey(key)){
                dict[key].Add(elem);
            }else{
                dict[key] = [elem];
            }
        }

        static public void AddCreate<K,V>(this Dictionary<K,List<V>> dict, K key, V elem) where K : notnull{
            if(dict.ContainsKey(key)){
                dict[key].Add(elem);
            }else{
                dict[key] = [elem];
            }
        }

        static public int Count<K,V>(this Dictionary<K,HashSet<V>> dict, K key) where K : notnull{
            if(dict.ContainsKey(key)){
                return dict[key].Count;
            }else{
                return 0;
            }
        }
    }

}