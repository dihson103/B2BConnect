import envConfig from '@/config'
import { ApiErrorResponse } from '@/types/util.types'

type CustomOptions = RequestInit & {
  baseUrl?: string | undefined
}

class HttpError extends Error {
  status: number
  payload: {
    message: string
    [key: string]: any
  }
  constructor({ status, payload }: { status: number; payload: any }) {
    super(`HTTP Error: ${status}`)
    this.status = status
    this.payload = payload
  }
}

const request = async <Response>(
  method: 'GET' | 'PUT' | 'POST' | 'DELETE',
  url: string,
  options?: CustomOptions
): Promise<Response> => {
  const body = options?.body ? JSON.stringify(options.body) : undefined
  const baseHeaders = {
    'Content-Type': 'application/json',
    ...options?.headers
  }

  const baseUrl = options?.baseUrl ?? envConfig.API_ENDPOINT
  const fullUrl = url.startsWith('/') ? `${baseUrl}${url}` : url

  const res = await fetch(fullUrl, {
    ...options,
    headers: baseHeaders,
    body,
    method
  })

  const payload: Response = await res.json()

  if (!res.ok) {
    var error = payload as ApiErrorResponse
    throw new Error(error.message)
  }

  return payload
}

const http = {
  get<Response>(url: string, options?: Omit<CustomOptions, 'body'>) {
    return request<Response>('GET', url, options)
  },
  post<Response>(url: string, body: any, options?: CustomOptions) {
    return request<Response>('POST', url, { ...options, body })
  },
  put<Response>(url: string, body: any, options?: CustomOptions) {
    return request<Response>('PUT', url, { ...options, body })
  },
  delete<Response>(url: string, body?: any, options?: CustomOptions) {
    return request<Response>('DELETE', url, { ...options, body })
  }
}

export default http
