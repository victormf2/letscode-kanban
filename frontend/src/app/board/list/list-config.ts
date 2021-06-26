import { Card } from "@/app/card/card";

export interface ListConfig {
    id: string
    title: string
    cards: Card[]
    allowAdd?: boolean
}