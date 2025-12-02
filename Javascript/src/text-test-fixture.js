import { Item } from './item';
import { GildedTros } from './gilded-tros';

console.log('AXXES CODE KATA - GILDED TROS');

const items = [
  new Item('Ring of Cleansening Code', 10, 20),
  new Item('Good Wine', 2, 0),
  new Item('Elixir of the SOLID', 5, 7),
  new Item('B-DAWG Keychain', 0, 80),
  new Item('B-DAWG Keychain', -1, 80),
  new Item('Backstage passes for Re:Factor', 15, 20),
  new Item('Backstage passes for Re:Factor', 10, 49),
  new Item('Backstage passes for HAXX', 5, 49),
  // these smelly items do not work properly yet
  new Item('Duplicate Code', 3, 6),
  new Item('Long Methods', 3, 6),
  new Item('Ugly Variable Names', 3, 6),
];

const app = new GildedTros(items);

let days = 4;
const args = process.argv.slice(2);
if (args.length > 0) {
  days = +args[0] + 1;
}

for (let i = 0; i < days; i++) {
  console.log('-------- day ' + i + ' --------');
  console.log('name, sellIn, quality');
  items.map((item) => item.toString()).forEach((item) => console.log(item));
  console.log();
  app.updateQuality();
}
