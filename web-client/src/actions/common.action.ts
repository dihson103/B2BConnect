import http, { CustomOptions } from '@/lib/http'
import { LoginResponse } from '@/types/auth.types'
import { cookies } from 'next/headers'

const action = {
  get<Response>(url: string, options?: Omit<CustomOptions, 'body'>) {
    const loginResponse = cookies().get('loginResponse')?.value
    if (!loginResponse) throw new Error('Không có quyền xem')
    try {
      const token: LoginResponse = JSON.parse(loginResponse)
      return http.get<Response>(url, {
        ...options,
        headers: {
          'Content-Type': 'application/json',
          Authorization: 'Bearer ' + token.accessToken
        }
      })
    } catch (error) {
      throw new Error('Không có quyền xem')
    }
  },
  post<Response>(url: string, body: any, options?: CustomOptions) {
    const loginResponse = cookies().get('loginResponse')?.value
    if (!loginResponse) throw new Error('Không có quyền xem')
    try {
      const token: LoginResponse = JSON.parse(loginResponse)
      return http.post<Response>(url, body, {
        ...options,
        headers: {
          'Content-Type': 'application/json',
          Authorization: 'Bearer ' + token.accessToken
        }
      })
    } catch (error) {
      throw new Error('Không có quyền xem')
    }
  },
  delete<Response>(url: string, body?: any, options?: CustomOptions) {
    const loginResponse = cookies().get('loginResponse')?.value
    if (!loginResponse) throw new Error('Không có quyền xem')
    try {
      const token: LoginResponse = JSON.parse(loginResponse)
      return http.delete<Response>(url, body, {
        ...options,
        headers: {
          'Content-Type': 'application/json',
          Authorization: 'Bearer ' + token.accessToken
        }
      })
    } catch (error) {
      throw new Error('Không có quyền xem')
    }
  },
  put<Response>(url: string, body: any, options?: CustomOptions) {
    const loginResponse = cookies().get('loginResponse')?.value
    if (!loginResponse) throw new Error('Không có quyền xem')
    try {
      const token: LoginResponse = JSON.parse(loginResponse)
      return http.put<Response>(url, body, {
        ...options,
        headers: {
          'Content-Type': 'application/json',
          Authorization: 'Bearer ' + token.accessToken
        }
      })
    } catch (error) {
      throw new Error('Không có quyền xem')
    }
  }
}

export default action
