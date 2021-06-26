export function closest(element: HTMLElement, parent: HTMLElement) {
  let elementToCheck: HTMLElement | null = element
  while (elementToCheck) {
    if (elementToCheck === parent) {
      return elementToCheck
    }
    elementToCheck = elementToCheck.parentElement
  }
  return null
}