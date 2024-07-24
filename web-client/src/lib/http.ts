import { handleAuthError } from '@/actions/auth.actions'
import envConfig from '@/config'
import { redirect } from 'next/navigation'

export type CustomOptions = RequestInit & {
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

  console.log('>>>> errr')

  if (!res.ok) {
    if (res.status === 401) {
      // await handleAuthError()
      throw new Error(`401|Token hết hạn`)
    }

    const error: any = await res.json()

    const message = `${error.status}|${error.message}`
    throw new Error(message)
  }

  const payload: Response = await res.json()

  console.log('>>>> payload', payload)

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
