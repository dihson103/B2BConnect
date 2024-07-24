import envConfig from '@/config'
import { NextResponse } from 'next/server'
import { getLoginResponseCookie } from './actions/auth.actions'
import { cookies } from 'next/headers'

export default async function middleware(request: Request) {
  if (request.url === `${envConfig.BASE_URL}/`) {
    return NextResponse.redirect(envConfig.HOME_URL)
  }
  if (request.url === envConfig.ADMIN_URL) {
    return NextResponse.redirect(envConfig.DASHBOARD_URL)
  }
  const loginResponse = await getLoginResponseCookie()
  const isAdmin = loginResponse?.account.isAdmin

  if (request.url === envConfig.LOGIN_URL || request.url === envConfig.REGISTER_URL) {
    if (loginResponse != null) {
      return NextResponse.redirect(isAdmin ? envConfig.BUSINESS_URL : envConfig.HOME_URL)
    }
  }

  if (request.url.startsWith(envConfig.ADMIN_URL)) {
    if (loginResponse == null) {
      return NextResponse.redirect(envConfig.LOGIN_URL)
    }

    if (!isAdmin) {
      return NextResponse.redirect(envConfig.HOME_URL)
    }
  }

  if (request.url.startsWith(envConfig.HOME_URL)) {
    if (loginResponse == null) {
      return NextResponse.redirect(envConfig.LOGIN_URL)
    }
  }

  return NextResponse.next()
}
