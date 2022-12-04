use std::{io::BufReader, fs::File, io};

use anyhow::Result;

fn main() -> Result<()> {
    let file = File::open("/home/siws/random/advent-of-code-2022/day4/input")?;
    let buf = BufReader::new(file);

    let input = io::read_to_string(buf)?;
    let input = input.lines();

    let total = input.into_iter().filter(|l| has_overlapping(l).unwrap()).count();

    println!("total: {:?}", total);

    return Ok(());
}

fn has_overlapping(line: &str) -> Result<bool> {
    let splitted: Vec<&str> = line.split(',').collect();

    let first_elf = get_range(splitted.first().unwrap())?;
    let last_elf = get_range(splitted.last().unwrap())?;

    return Ok(overlaps(first_elf, last_elf));
}

fn get_range(section: &str) -> Result<Vec<u32>> {
    let splitted: Vec<&str> = section.split('-').collect();

    let first: u32 = splitted.first().unwrap().parse()?;
    let last: u32 = splitted.last().unwrap().parse()?;

    let values: Vec<u32> = (first..=last).collect();

    return Ok(values);
}

fn overlaps(first: Vec<u32>, last: Vec<u32>) -> bool {
    for el in &last {
        if first.contains(&el) {
            return true;
        }
    }

    for el in first {
        if last.contains(&el) {
            return true;
        }
    }

    return false;
}

