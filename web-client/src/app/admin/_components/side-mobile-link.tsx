'use client'

import Link from 'next/link'
import { usePathname } from 'next/navigation'
import React, { ReactNode } from 'react'

type Props = {
  displayValue: string
  url: string
  children: ReactNode
}

export default function AppSideMobileLink({ displayValue, url, children }: Props) {
  const pathname = usePathname()
  return (
    <Link
      href={url}
      className={`flex items-center gap-4 px-2.5 ${pathname === url ? 'text-foreground' : 'text-muted-foreground hover:text-foreground'}`}
    >
      {children}
      {displayValue}
    </Link>
  )
}
