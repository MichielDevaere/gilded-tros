package com.gildedtros

class GildedTros(var items: Array<Item>) {

    fun updateQuality() {
        for (i in items.indices) {
            if (items[i].name != "Good Wine" && items[i].name != "Backstage passes for Re:Factor"
            && items[i].name != "Backstage passes for HAXX"){
                if (items[i].quality > 0) {
                    if (items[i].name != "B-DAWG Keychain") {
                        items[i].quality = items[i].quality - 1
                    }
                }
            } else {
                if (items[i].quality < 50) {
                    items[i].quality = items[i].quality + 1

                    if (items[i].name == "Backstage passes for Re:Factor") {
                        if (items[i].sellIn < 11) {
                            if (items[i].quality < 50) {
                                items[i].quality = items[i].quality + 1
                            }
                        }

                        if (items[i].sellIn < 6) {
                            if (items[i].quality < 50) {
                                items[i].quality = items[i].quality + 1
                            }
                        }
                    }
                }
            }

            if (items[i].name != "B-DAWG Keychain") {
                items[i].sellIn = items[i].sellIn - 1
            }

            if (items[i].sellIn < 0) {
                if (items[i].name != "Good Wine") {
                    if (items[i].name != "Backstage passes for Re:Factor" || items[i].name != "Backstage passes for HAXX") {
                        if (items[i].quality > 0) {
                            if (items[i].name != "B-DAWG Keychain") {
                                items[i].quality = items[i].quality - 1
                            }
                        }
                    } else {
                        items[i].quality = items[i].quality - items[i].quality
                    }
                } else {
                    if (items[i].quality < 50) {
                        items[i].quality = items[i].quality + 1
                    }
                }
            }
        }
    }

}
