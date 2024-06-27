import envConfig from '@/config'
import { NextResponse } from 'next/server'

export default function middleware(request: Request) {
  console.log('url', request.url)
  if (request.url === `${envConfig.BASE_URL}/`) {
    return NextResponse.redirect(envConfig.HOME_URL)
  }
  if (request.url === envConfig.ADMIN_URL) {
    return NextResponse.redirect(envConfig.DASHBOARD_URL)
  }
  return NextResponse.next()
}
