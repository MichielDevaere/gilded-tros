package com.gildedtros

import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

internal class GildedTrosTest {

    @Test
    fun foo() {
        val items = arrayOf<Item>(Item("foo", 0, 0))
        val app = GildedTros(items)
        app.updateQuality()
        assertEquals("fixme", app.items[0].name)
    }

}
