import { Injectable } from "@angular/core";
import { ListConfig } from "./list-config";

@Injectable()
export class ListContextContainer {
  listContext?: ListContext
}

//@Injectable()
export class ListContext {
  constructor(
    readonly current: ListConfig,
    readonly first: ListConfig,
    readonly last: ListConfig,
    readonly next: ListConfig | null,
    readonly previous: ListConfig | null,
  ) {

  }
}