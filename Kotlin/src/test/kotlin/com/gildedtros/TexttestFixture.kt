package com.gildedtros

fun main(args: Array<String>) {

    println("AXXES CODE KATA - GILDED TROS")

    val items = arrayOf(
            Item("Ring of Cleansening Code", 10, 20),
            Item("Good Wine", 2, 0),
            Item("Elixir of the SOLID", 5, 7),
            Item("B-DAWG Keychain", 0, 80),
            Item("B-DAWG Keychain", -1, 80),
            Item("Backstage passes for Re:Factor", 15, 20),
            Item("Backstage passes for Re:Factor", 10, 49),
            Item("Backstage passes for HAXX", 5, 49),
            // these smelly items do not work properly yet
            Item("Duplicate Code", 3, 6),
            Item("Long Methods", 3, 6),
            Item("Ugly Variable Names", 3, 6))

    val app = GildedTros(items)

    var days = 2
    if (args.size > 0) {
        days = Integer.parseInt(args[0]) + 1
    }

    for (i in 0..days - 1) {
        println("-------- day $i --------")
        println("name, sellIn, quality")
        for (item in items) {
            println(item)
        }
        println()
        app.updateQuality()
    }

}
