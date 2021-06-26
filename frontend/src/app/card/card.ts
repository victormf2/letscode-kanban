export interface NewCard {
  title: string
  content: string
  listId: string
}

export interface Card {
  id: string
  title: string
  content: string
  listId: string
}

export interface CardListResult {
  cards: Card[]
}