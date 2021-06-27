import { API_BASE_URL } from "@/env";

export function api(route: string): string {
    return `${API_BASE_URL}/${route}`
}