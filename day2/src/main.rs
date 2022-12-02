use anyhow::Result;
use std::{fs::File, io::{BufReader, self}};

fn main() -> Result<()> {
    let file = File::open("/home/siws/random/advent-of-code-2022/day2/input")?;
    let buf = BufReader::new(file);

    let input = io::read_to_string(buf)?;
    let input = input.lines();

    let total = input
        .into_iter()
        .map(|l| Round::new(l).unwrap().calculate())
        .sum::<usize>();

    println!("total: {}", total);

    Ok(())
}

#[derive(PartialEq, Debug)]
enum Shape {
    Rock,
    Paper,
    Scissors
}

impl Shape {
    fn new(input: &str) -> Self {
        match input {
            "X" | "A" => Self::Rock,
            "Y" | "B" => Self::Paper,
            "Z" | "C" => Self::Scissors,
            _ => unreachable!()
        }
    }

    fn get_points(&self) -> u8 {
        match self {
            Shape::Rock => 1,
            Shape::Paper => 2,
            Shape::Scissors => 3,
        }
    }

    fn check_if_beats(&self, input: &Shape) -> bool {
        let weaker_shape = match self {
            Shape::Rock => Shape::Scissors,
            Shape::Paper => Shape::Rock,
            Shape::Scissors => Shape::Paper, 
        }; 

        weaker_shape == *input
    }
}

struct Round {
    opponent: Shape,
    me: Shape,
}

#[derive(Debug)]
enum RoundResult {
    Win,
    Loss,
    Draw
}

impl RoundResult {
    fn get_points(&self) -> u8 {
        match self {
            RoundResult::Win => 6,
            RoundResult::Loss => 0,
            RoundResult::Draw => 3,
        }
    }
}

impl Round {
    fn new(line: &str) -> Option<Self> {
        let splitted = line.split_whitespace().collect::<Vec<&str>>();

        let opponent = Shape::new(splitted.first()?);
        let me = Shape::new(splitted.last()?);

        Some(Self {
            me,
            opponent
        })
    }

    fn calculate(&self) -> usize {
        let shape_points = self.me.get_points();
        let result_points = self.get_result().get_points();

        (shape_points + result_points).into()
    }

    fn get_result(&self) -> RoundResult {
        let i_win = self.me.check_if_beats(&self.opponent);
        let opponent_wins = self.opponent.check_if_beats(&self.me);

        if i_win {
            RoundResult::Win
        } else if opponent_wins {
            RoundResult::Loss
        }
        else {
            RoundResult::Draw 
        }
    }
}

