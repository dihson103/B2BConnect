'use client'

import { DropdownMenuItem } from '@/components/ui/dropdown-menu'
import { useRouter } from 'next/navigation'

export default function LogoutButton() {
  const router = useRouter()
  const handleLogout = () => {
    router.push('/logout')
  }
  return <DropdownMenuItem onClick={handleLogout}>Logout</DropdownMenuItem>
}
