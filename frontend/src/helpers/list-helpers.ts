export function groupBy<T>(list: T[], keySelector: (t: T) => string) {
    const mapping: { [key: string]: T[] } = {}
    for (let item of list) {
        const key = keySelector(item)
        if (!mapping[key]) {
            mapping[key] = []
        }
        mapping[key].push(item)
    }
    return mapping
}