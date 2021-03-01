package Phase01.test;

import Phase01.src.Entry;

import static org.junit.jupiter.api.Assertions.assertEquals;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.BeforeEach;

public class EntryTest {
    private Entry entry;

    @BeforeEach
    public void createObject() throws Exception{
        entry = new Entry("test", 11);
    }

    @Test
    public void toStringTest(){
       
        assertEquals(entry.toString(), "Document Id: test, Index: 11");
    }

    @Test
    public void equalsTest(){
        Entry obj = new Entry("test", 11);
        assertEquals(obj.equals(entry) , true);
    }

    @Test
    public void hashCodeTest(){
        assertEquals(entry.hashCode() , "test".hashCode());
    }



}
