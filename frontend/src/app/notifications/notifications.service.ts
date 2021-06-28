import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

export type NotificationType = 'info' | 'success' | 'warning' | 'error'

const notificationCssClasses: { [key in NotificationType]: string } = {
  info: 'is-info',
  success: 'is-success',
  warning: 'is-warning',
  error: 'is-danger'
}

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  constructor() { }

  show(type: NotificationType, message: any, duration?: number) {
    if (!message) {
      return
    }
    const container = document.getElementById('notifications')
    if (!container) {
      return
    }
    const notificationElement = this.builbNotificationElement(type, message)
    container.appendChild(notificationElement)
    setTimeout(() => {
      notificationElement.remove()
    }, duration || 4000)
  }

  private builbNotificationElement(type: NotificationType, message: any): HTMLElement {
    
    const rootElement = document.createElement('div')
    const notificationClass = notificationCssClasses[type]
    rootElement.classList.add('notification', notificationClass)
    
    const closeButton = document.createElement('button')
    closeButton.classList.add('delete')
    closeButton.onclick = () => rootElement.remove()

    const contentElement = document.createElement('p')
    contentElement.textContent = this.getTextContent(message)

    rootElement.appendChild(closeButton)
    rootElement.appendChild(contentElement)

    return rootElement
  }

  private getTextContent(messageObject: any): string {
    if (typeof messageObject === 'string') {
      return messageObject
    }

    if (messageObject instanceof HttpErrorResponse) {
      if (messageObject.status === 0) {
        return 'Service unavailable. Please try again later.'
      }
      return this.getTextContent(messageObject.error)
    }

    if (messageObject.message) {
      return this.getTextContent(messageObject.message)
    }

    return messageObject.toString()
  }
}
