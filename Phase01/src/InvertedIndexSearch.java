package Phase01.src;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Scanner;
import java.util.Set;

public class InvertedIndexSearch {
    public static HashMap<String, ArrayList<String>> indexMap = new HashMap<String, ArrayList<String>>();

    final static Set<String> stopWords = new HashSet<>(Arrays.asList("ourselves", "hers", "between", "yourself", "but",
            "again", "there", "about", "once", "during", "out", "very", "having", "with", "they", "own", "an", "be",
            "some", "for", "do", "its", "yours", "such", "into", "of", "most", "itself", "other", "off", "is", "s",
            "am", "or", "who", "as", "from", "him", "each", "the", "themselves", "until", "below", "are", "we", "these",
            "your", "his", "through", "don", "nor", "me", "were", "her", "more", "himself", "this", "down", "should",
            "our", "their", "while", "above", "both", "up", "to", "ours", "had", "she", "all", "no", "when", "at",
            "any", "before", "them", "same", "and", "been", "have", "in", "will", "on", "does", "yourselves", "then",
            "that", "because", "what", "over", "why", "so", "can", "did", "not", "now", "under", "he", "you", "herself",
            "has", "just", "where", "too", "only", "myself", "which", "those", "i", "after", "few", "whom", "t",
            "being", "if", "theirs", "my", "against", "a", "by", "doing", "it", "how", "further", "was", "here",
            "than"));

    public static void main(String[] args) {
        ReadFiles();

        // int i = 0;
        // for (String word : indexMap.keySet()) {
        //     System.out.println(word);
        //     System.out.println(indexMap.get(word).toString());
        //     if (++i > 5)
        //         break;
        // }
    }

    public static void ReadFiles() {
        File directory = new File("Phase01/data/EnglishData");
        File fileList[] = directory.listFiles();
        for (File file : fileList) {
            String id = file.getName();
            try {
                Scanner scanner = new Scanner(file);
                while (scanner.hasNextLine()) {
                    String content = scanner.nextLine();
                    addWords(id, content);
                }
                scanner.close();
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            }
        }
    }

    public static void addWords(String id, String content) {
        String[] listOfWords = getListOfWords(content);
        for (String word : listOfWords) {
            word = word.toLowerCase();
            if (stopWords.contains(word))
                continue;
            if (indexMap.containsKey(word)) {
                indexMap.get(word).add(id);
            } else {
                indexMap.put(word, new ArrayList<String>(Arrays.asList(id)));
            }
        }
    }

    public static String[] getListOfWords(String content) {
        return content.split("\\s+");
    }

}
